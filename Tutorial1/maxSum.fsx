let seq0 = [ 1; 2; 3; 4; 5 ]
let seq1 = [ 1; 2; -2; 4; 5 ]
let seq2 = [ 1; 2; -3; 4; 5 ]
let seq3 = [ 1; 2; -4; 4; 5 ]
let seq4 = [ 1; 2; -21; 4; 5; -23; 1; 1 ]
let seq5 = [ 1; 2; -21; 4; 5; -23; 12; 1 ]
let seq6 = [ 1; 2; -21; 4; 5; -23; 12; -1 ]
let seq7 = [ 1; 2; -21; 4; 5; -23; 12; -13 ]
let seq8 = [ 1; 2; -21; 4; 5; -23; 12; 0 ]

let rec maxSubSeq items best temp =
    match items with
    | [] -> best
    | item :: rest -> 
        let newTemp = max (temp + item) 0
        let newBest = max best newTemp
        maxSubSeq rest newBest newTemp

let rec maxSubSeqInterval items current (best, bFirst, bLast) (temp, tFirst, tLast) =
    match items with
    | [] -> best, bFirst, bLast
    | item :: rest ->
        let newTemp = max (temp + item) 0
        let newBest = max best newTemp
        let next = current + 1
        match newTemp with
        | 0 -> maxSubSeqInterval rest next (best, bFirst, bLast) (0, next, next)
        | _ -> 
            if newBest = newTemp then
                maxSubSeqInterval rest next (newBest, tFirst, current) (newTemp, tFirst, current)
            else
                maxSubSeqInterval rest next (best, bFirst, bLast) (newTemp, tFirst, current)


let printResult (items: int list) =
    printfn "the sum for %A is %i" items (maxSubSeq items 0 0)

let printWithInterval items = 
    let best, first, last = maxSubSeqInterval items 1 (0,0,0) (0,0,0)
    printfn "the sum for %A is %i in interval (%i, %i)" items best first last

printResult seq0
printResult seq1
printResult seq2
printResult seq3
printResult seq4
printResult seq5
printResult seq6
printResult seq7

printfn ""

printWithInterval seq0
printWithInterval seq1
printWithInterval seq2
printWithInterval seq3
printWithInterval seq4
printWithInterval seq5
printWithInterval seq6
printWithInterval seq7
printWithInterval seq8