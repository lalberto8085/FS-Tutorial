let sumFirst n =
    (n * (n+1)) / 2

let sumFirstSquares n =
    [1..n] |> List.map float |> List.fold (fun sum i -> sum + i**2.0) 0.0 

let squaringDifference n =
    int <| (float (sumFirst n))**2.0 - (sumFirstSquares n)

printfn "the difference for %i is %i" 10 (squaringDifference 10)
printfn "the difference for %i is %i" 100 (squaringDifference 100)