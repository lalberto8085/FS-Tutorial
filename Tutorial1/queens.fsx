type Queen = {x :int; y :int}

let size =  25;

let isSafe x y (queens: Queen list) = 
    not(queens |> List.exists (fun q -> q.x = x || q.y = y || abs(q.x - x) = abs(q.y - y)))
      
let rec placeInRow (queens: Queen list) y = 
    if queens.Length = size then
        queens, true
    else
        let sequences = [0..size-1] 
                              |> List.filter (fun x -> isSafe x y queens) 
                              |> Seq.map (fun x -> placeInRow ({x=x; y=y} :: queens) (y+1))
        match sequences |> Seq.tryFind (fun (_, solved) -> solved) with
        | Some result -> result
        | None        -> [], false

let rec placeInCol (queens:Queen list) x =
    if queens.Length = size then
        queens, true
    else
        let sequences = [0..size-1] 
                            |> List.filter (fun y -> isSafe x y queens) 
                            |> Seq.map (fun y -> placeInCol ({x=x; y=y} :: queens) (x+1))
        match sequences |> Seq.tryFind (fun (_, solved) -> solved) with
        | Some result -> result
        | None        -> [], false
        
let printBoard (queens: Queen list) =
    let separator = String.replicate (size * 4 + 1) "-"
    printfn "%s" separator
    for y in [0..size-1] do
        printf "|"
        for x in [0..size-1] do
            match queens |> List.exists (fun q -> q.x = x && q.y = y) with
            | true -> printf " X |" 
            | false -> printf " 0 |"
        printfn "" 
        printfn "%s" separator


#time
let res, _ = placeInRow [] 0
printBoard res
#time