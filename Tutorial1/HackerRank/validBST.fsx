open System

let getValues (input : string) = 
    input.Split([|' '|]) |> Seq.map int |> Seq.toList

let rec parseTree leftPs rightPs values = 
    match values with
    | [] -> true, []
    | h :: rest ->
        let isValid = List.forall (fun lp -> h > lp) leftPs && 
                      List.forall (fun rp -> h < rp) rightPs
        match isValid with
        | false -> false, values
        | true ->
            let cLeft, rLeft = parseTree leftPs (h :: rightPs) rest
            let cRight, rRight = parseTree (h :: leftPs) rightPs rLeft
            cLeft || cRight, rRight

let canParseTree values =
    let result, _ = parseTree [] [] values
    result

let parseCase i =
    ignore <| Console.ReadLine();
    Console.ReadLine() |> getValues

let casesCount = int <| Console.ReadLine()
let cases = [1..casesCount] |> List.map parseCase |> List.map canParseTree
cases |> List.iter (fun b -> if b  then printfn "YES" else printfn "NO")
