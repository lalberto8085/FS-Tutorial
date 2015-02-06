
let rec mcd a b =
    if b = 0 then
        a
    else
        mcd b (a%b)

let mcm a b =
    a / (mcd a b) * b

let minimumForSet min max =
    List.fold (fun cumul current -> mcm cumul current) 1 [min..max]

printfn "the number up to 10 is %i" (minimumForSet 1 10)
#time
printfn "the number up to 20 is %i" (minimumForSet 1 20)
#time