let loggerFunc anyFunc input = 
    printfn "input is %A" input
    let result = anyFunc input;
    printfn "The result is %A" result
    result

let plusOne input = input + 1
let multFunc (x, y)  = x * y

let loggingPlus = loggerFunc plusOne 
let loggingMult = loggerFunc multFunc

loggingPlus 12
let r = loggingMult (3, 4)

let fb i =
    if(i % 15 = 0) then
        "FizzBuzz"
    else if(i % 5 = 0) then
        "Buzz"
    else if(i % 3 = 0) then
        "Fizz"
    else
        i.ToString()

let printfb i =
    let s = fb i
    printfn "%s" s

[1..100] |> List.iter (fun i -> printfn "%s" (fb i))
[1..100] |> List.iter printfb
