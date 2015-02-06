open System
open System.Collections.Specialized

let calculateMedian (values :int list) = 
    let candidate = values.Length / 2
    match values.Length % 2 = 0 with
    | true -> min values.[candidate] values.[candidate - 1]
    | _    -> values.[candidate]

let updateCache (cache :(int list * int) array) returns values index median =
    ignore <| match List.tryFind (fun x -> x = index) returns with
              | Some _ -> cache.[index] <- (values, median)
              | None   -> cache.[index] <- ([], 0)

let calculateMedianAndValues cache returns values index case =
    if case > 0 then
        let newValues = case :: values //|> List.sort
        let median = calculateMedian newValues
        updateCache cache returns newValues index median
        median, newValues
    else
        let newValues, median = cache.[index+case]
        updateCache cache returns newValues index median
        median, newValues

let rec solve cache cases returns (results :int array) values index =
    match cases with
    | [] -> results
    | case :: rest ->
        let median, newValues = calculateMedianAndValues cache returns values index case
        results.[index] <- median
        solve cache rest returns results newValues (index+1)
        

let caseCount = 100000
let cases = [1..caseCount]

//let caseCount = 10
//let cases = [1;5;-2;3;2;5;4;-7;2;-3]

//let caseCount = int <| Console.ReadLine()
//let cases = [1..caseCount] |> List.map (fun _ -> int <| Console.ReadLine())

let cache : (int list * int)array = Array.init caseCount (fun i -> [],0)
let medians = Array.init caseCount (fun i -> 0)

#time
// indexes where there is a jump-back 
let returns = cases |> List.mapi (fun i x -> i, x) 
                    |> List.filter (fun (i, x) -> x < 0) 
                    |> List.map (fun (i,x) -> i + x)
                    |> Seq.distinct 
                    |> Seq.toList
#time

#time
solve cache cases returns medians [] 0 
#time

medians |> Seq.iter (printfn "%d")