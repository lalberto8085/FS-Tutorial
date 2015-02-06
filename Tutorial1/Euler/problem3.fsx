let value = 600851475143L

let getPrimeProspect index =
    match index with
    | 0 -> 2L
    | 1 -> 3L
    | x -> int64 <| 6 * (x / 2) + if x % 2 = 0 then -1 else 1
    
let rec maxPrimeFactorRec input index =
    let candidate = getPrimeProspect index
    if candidate = input then 
        candidate
    else if candidate > input then 
        0L
    else
        let result, rest = input / candidate, input % candidate
        match rest with
        | 0L -> max candidate (maxPrimeFactorRec result index)
        | _  -> maxPrimeFactorRec input (index+1)

let maxPrimeFactor value =
    maxPrimeFactorRec value 0

printfn "%i" (maxPrimeFactor 13195L)

#time
printfn "%i" (maxPrimeFactor value)
#time