type Address = { Street: string ; City: string }
type Customer = {Id: int; Name: string; Address: Address}


let customer1 = {Id = 1; Name= "Jhon"; Address = {Street = "Asdf"; City = "Frisco"}}

let {Name = name1} = customer1
printfn "the name is %s" name1

let {Name = name2; Id = id2} = customer1
printfn "customer id: %i and name: %s" id2 name2

let {Name = name3; Address = {Street = street3}} = customer1
printfn "customer %s lives in %s street" name3 street3
