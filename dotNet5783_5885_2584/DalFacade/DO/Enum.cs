﻿namespace DO;

public struct Enums
{
    public enum Category
    {

    }
    public enum Entity
    { 
        Exit, Products, Orders, OrderItems
    };
    public enum CRUDOp
    {
       Exit, Create,Read,ReadAll,  Delete, Update, ReadByProductAndOrder, ReadAllByOrder 
    }
}

