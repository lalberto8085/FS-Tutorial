open System
open System.Collections.Generic

let m = 10I**8 + 7I
let mem = new Dictionary<int, bigint>()

let rec memFib n =
    match mem.TryGetValue(n) with
    | true, value -> value
    | _ -> 
        let value = fib n
        mem.Add(n, value)
        value
and fib n =
    match n with
    | 0 -> 0I
    | 1 -> 1I
    | x -> memFib (x-1) + memFib (x-2)

let rec readValues values n = 
    if n = 0 then
        values |> List.rev
    else
        let c = int <| Console.ReadLine()
        readValues (c :: values) (n-1)

let count = int <| Console.ReadLine()
let values = readValues [] count
values |> List.map (fun n -> (memFib n) % m) |> List.iter (printfn "%A")
