
let getPrimeProspect index =
    match index with
    | 0 -> 2
    | 1 -> 3
    | x -> 6 * (x / 2) + if x % 2 = 0 then -1 else 1

let rec getPrime count prospectIndex found =
    let prospect = getPrimeProspect prospectIndex
    if count = 0 then
        getPrimeProspect (prospectIndex - 1)
    else
        match found |> Seq.exists (fun p -> prospect % p = 0 && p <> prospect) with
        | true -> getPrime count (prospectIndex+1) found
        | false -> getPrime (count-1) (prospectIndex+1) (prospect::found)

let findPrime n =
    getPrime n 0 []

printfn "the 6th prime is %i" (findPrime 6)
#time
printfn "the 10 001 st prime is %i" (findPrime 10001)
#time