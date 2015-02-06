
let rec calculate sum previous last max =
    let current = last + previous
    let newSum = if current % 2 = 0 then current else sum
    if current >= max then
        newSum
    else
        calculate newSum last current max

let printResult max =
    let res = calculate 0 1 1 max
    printfn "the even Fibonacci numbers sum until %i is %i" max res

#time
printResult 4000000
#time