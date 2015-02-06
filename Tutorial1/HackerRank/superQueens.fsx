open System

type Queen = {x:int; y:int}

let threatens q1 q2 =
    let rows = abs (q1.x - q2.x)
    let cols = abs (q1.y - q2.y)
    rows = 0 || cols = 0 || rows = cols || (rows = 1 && cols = 2) || (rows = 2 && cols = 1)

let isSafe queens queen =
    not <| List.exists (threatens queen) queens

let rec countSetOnRow size row queens =
    let available = [1..size] |> List.map (fun i -> {x=row;y=i})
                              |> List.filter (isSafe queens)
    if available.Length = 0 then
        0
    else if queens.Length = size - 1 then
        available.Length
    else
        List.fold (fun cum q -> cum + countSetOnRow size (row+1) (q:: queens)) 0 available

printfn "Please type the number of Queens: "
let size = int <| Console.ReadLine()

#time
let r = countSetOnRow size 0 []
#time
printfn "%i" r
