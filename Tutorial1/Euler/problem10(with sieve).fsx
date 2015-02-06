let getPrimeProspect index =
    match index with
    | 0 -> 2
    | 1 -> 3
    | x -> 6 * (x / 2) + if x % 2 = 0 then -1 else 1

    

let calculate max =
    let upper = [1..max/3] |> Seq.find (fun i -> getPrimeProspect i >= max) 
    let marked = Array.init (upper - 1) (fun _ -> true)
    let root = int <| sqrt (float max)
    3

