open System
open System.Collections.Generic

let calculateMedian (values :int list) = 
    let candidate = values.Length / 2
    match values.Length % 2 = 0 with
    | true -> min values.[candidate] values.[candidate - 1]
    | _    -> values.[candidate]

let updateCache (cache :Dictionary<int, int list * int>) (returns :Queue<int>) values index median =
    if returns.Count > 0 && returns.Peek() = index then
        cache.[returns.Dequeue()] <- (values, median)

let calculateMedianAndValues cache returns values index case =
    if case > 0 then
        let newValues = case :: values |> List.sort
        let median = calculateMedian newValues
        updateCache cache returns newValues index median
        median, newValues
    else
        let newValues, median = cache.[index+case]
        updateCache cache returns newValues index median
        median, newValues

let rec solve cache cases (returns :Queue<int>) results values index =
    match cases with
    | [] -> results |> List.rev
    | case :: rest ->
        let median, newValues = calculateMedianAndValues cache returns values index case
        solve cache rest returns (median :: results) newValues (index+1)
        

//let caseCount = 100000
//let cases = [1..caseCount]

let caseCount = 10
let cases = [1;5;-2;3;2;5;4;-7;2;-3]

//let caseCount = int <| Console.ReadLine()
//let cases = [1..caseCount] |> List.map (fun _ -> int <| Console.ReadLine())

let cache = new Dictionary<int, int list * int>()

let folder (queue :Queue<int>) item =
    if not <| queue.Contains(item) then
        queue.Enqueue(item)
    queue

// indexes where there is a jump-back 
let returns = cases |> List.mapi (fun i x -> i, x) 
                    |> List.filter (fun (i, x) -> x < 0) 
                    |> List.map (fun (i,x) -> i + x)
                    |> Seq.fold folder (new Queue<int>())

#time
let medians = solve cache cases returns [] [] 0 
#time

medians |> Seq.iter (printfn "%d")