namespace Somerpg.Server.Model

open System.Runtime.Serialization


[<DataContract>]
type TestModel =
    {
        [<field: DataMember(Name = "Message")>]
        Message : string
    }
