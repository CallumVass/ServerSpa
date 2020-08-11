module Model

open System

[<CLIMutable>]
type PostModel =
    { FavouriteNumber: int
      Name: string
      Date: DateTime
      IsActive: bool }
