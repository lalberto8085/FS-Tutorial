
let makePalindrome seed =
    let str = string seed
    let length = str.Length - 1
    let reverse = List.fold (fun acc i -> acc + (string str.[length - i])) "" [0..length] 
    int <| str + reverse

let getFactors value max =
    let root = int <| sqrt (float value)
    match [root..max] |> Seq.tryFind (fun x -> value % x = 0 && value / x < max) with
    | Some x -> true, value, x
    | None   -> false, 0, 0

let largestPalindrome min max =
    let found = [max.. -1..min] |> Seq.map (fun x -> makePalindrome x )
                                |> Seq.map (fun pal -> getFactors pal max)
                                |> Seq.tryFind (fun (r, _, _) -> r)
    match found with
    | Some (_, pal, factor) -> pal, factor, (int pal) / factor
    | None -> 0, 0, 0

#time
let pal, f1, f2 = largestPalindrome 100 999
#time
printfn "the largest palindrome is %i and it's factors are %i and %i" pal f1 f2
