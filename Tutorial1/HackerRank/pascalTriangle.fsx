open System

let makeList some = 
    match some with
    | [] -> [1]
    | [_] -> [1;1]
    | [h; t] -> [1; h + t; 1]
    | h :: rest -> 
        let summed = rest |> List.mapi (fun i x -> if i = 0 then h + x else rest.[i-1] + x )
        1 :: summed @ [1]

let printRow row =
    let str = List.fold (fun sum i -> sum + (string i) + " ") "" row
    printfn "%s" str

let getNextRow row =
    let nextRow = makeList row
    printRow nextRow
    nextRow

let main =
    let count = Console.ReadLine() |> int
    ignore <| List.fold (fun row _ -> getNextRow row ) [] [1..count]
    0
        