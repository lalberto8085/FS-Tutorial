
let getPrimeProspect index =
    match index with
    | 0 -> 2L
    | 1 -> 3L
    | x -> int64 <| 6 * (x / 2) + if x % 2 = 0 then -1 else 1

let isPrime primes n =
    not <| List.exists (fun p -> n % p = 0L) primes

let rec sumPrimesUnder maxValue sum index found =
    let candidate = getPrimeProspect index
    if candidate > maxValue then
        sum
    else
        match isPrime found candidate with
        | true -> sumPrimesUnder maxValue (sum + candidate) (index + 1) (candidate :: found)
        | false -> sumPrimesUnder maxValue (sum) (index + 1) (found)

let calculate n =
    sumPrimesUnder n 0L 0 []

#time
printfn "the sum of primes below %i is %i" 10L (calculate 10L)
#time
#time
printfn "the sum of primes below %i is %i" 2000000L (calculate 2000000L)
#time