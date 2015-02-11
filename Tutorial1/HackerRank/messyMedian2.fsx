open System
open System.Collections.Generic

let calculateNewValuesAndMedian (minValues : SortedSet<int>) (maxValues : SortedSet<int>) case =
    if(minValues.Count = 0) then
        minValues.Add(case) |> ignore
    else
//        printfn "max.min: %i; case: %i" maxValues.Min case
        if case > minValues.Max then // adding to max
            if maxValues.Count > minValues.Count then
                minValues.Add(maxValues.Min) |> ignore
                maxValues.Remove(maxValues.Min) |> ignore
            maxValues.Add(case) |> ignore
        else // adding to min
            if minValues.Count > maxValues.Count then
                maxValues.Add(minValues.Max) |> ignore
                minValues.Remove(minValues.Max) |> ignore
            minValues.Add(case) |> ignore
    let median = if maxValues.Count > minValues.Count 
                    then maxValues.Min 
                    else minValues.Max
    minValues, maxValues, median

let updateCache (cache: Dictionary<int, int list * int list * int>) (jumpIndexes : Queue<int>)(minValues : SortedSet<int>) (maxValues : SortedSet<int>) median index =
    if jumpIndexes.Count > 0 && jumpIndexes.Peek() = index then
        let mutable minArr = Array.init minValues.Count (fun _ -> 0)
        minValues.CopyTo(minArr)
        let mutable maxArr = Array.init maxValues.Count (fun _ -> 0)
        maxValues.CopyTo(maxArr)
        cache.Add(jumpIndexes.Dequeue(), (minArr |> Array.toList, maxArr |> Array.toList, median))

let calculateMedianAndValues cache jumpIndexes minValues maxValues index case = 
    if case > 0 then
        let newMins, newMaxs, median = calculateNewValuesAndMedian minValues maxValues case
        updateCache cache jumpIndexes newMins newMaxs median index
        printfn "min: %A; max: %A; median: %i; case: %i; index: %i" newMins newMaxs median case index
        median, newMins, newMaxs
    else
        let minsArr, maxArr, median = cache.[index+case]
        let newMins = new SortedSet<int>(minsArr)
        let newMaxs = new SortedSet<int>(maxArr)
        updateCache cache jumpIndexes newMins newMaxs median index
        printfn "min: %A; max: %A; median: %i; case: %i; index: %i" newMins newMaxs median case index
        median, newMins, newMaxs

let rec solve cache cases jumpIndexes results minValues maxValues index =
    match cases with
    | [] -> results |> List.rev
    | case :: rest ->
        let median, mins, maxs = calculateMedianAndValues cache jumpIndexes minValues maxValues index case
        solve cache rest jumpIndexes (median :: results) mins maxs (index+1)

//let caseCount = 10
//let cases = [1;5;-2;3;2;5;4;-7;2;-3]

//let caseCount = 10
//let cases = [1..caseCount]

//let caseCount = 100000
//let cases = [1..caseCount]

//let caseCount = 10
//let cases = [1000093; 1000080; 1000055; 1000092; 1000039; -1; -3; -5; -7; -9]

let caseCount = 20
let cases = [19; 19;-1; 18; 19; 17; -6; 18; -1; -5; -4; 16; 17; 17; 16; 19; 20; 18; 15; -17]

//let caseCount = int <| Console.ReadLine()
//let cases = [1..caseCount] |> List.map (fun _ -> int <| Console.ReadLine())

let cache = new Dictionary<int, int list * int list * int>()

// indexes where there is a jump-back 
let jumpBacks = cases |> Seq.mapi (fun i x -> i, x) 
                      |> Seq.filter (fun (i, x) -> x < 0) 
                      |> Seq.map (fun (i,x) -> i + x)
                      |> Seq.distinct
                      |> Seq.sort

let jumpIndexes = new Queue<int>(jumpBacks)

#time
let medians = solve cache cases jumpIndexes [] (new SortedSet<int>()) (new SortedSet<int>()) 0 
#time

medians |> Seq.iter (printfn "%d")