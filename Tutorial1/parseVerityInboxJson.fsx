#r @"C:\Users\Luiso\Documents\Visual Studio 2013\Projects\Projects\Tutorial1\Tutorial1\bin\Debug\Newtonsoft.Json.dll"

open System
open Newtonsoft.Json
open System.IO

let file = @"C:\Users\Luiso\Desktop\Copy of inbox.json"

type SMS = 
    { body : string
      id : string
      dateDelivered : Nullable<DateTime>
      sourceAddress : string
      destAddress : string
      dateCreated : DateTime
      isRead : bool }

type Inbox = {total: int; response: SMS list; sessions: string; success: bool; size: int}

let fileContent = File.ReadAllText(file)
let inbox = JsonConvert.DeserializeObject<Inbox>(fileContent)

let newFile = @"C:\Users\Luiso\Desktop\messages.json"

File.WriteAllText(newFile, JsonConvert.SerializeObject(inbox.response))