
let fizzBuzzSum n =
    let sum = [1..n-1] |> List.fold (fun cum i -> if i % 3 = 0 || i % 5 = 0 then cum + i else cum) 0
    printfn "the fizz-buzz sum until %i is %i" n sum

fizzBuzzSum 10
fizzBuzzSum 100