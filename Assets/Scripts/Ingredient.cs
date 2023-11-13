using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient
{
    public IngredientType type;
    public string name;

    public override string ToString()
    {
        return name;
    }
}

public enum IngredientType
{
    Flour,
    Sugar,
    Milk,
    Egg,
    Frosting,
    Chocolate,
    Vanilla,
    Sprinkles,
    Cake
}

public class IngredientList
{
    public Ingredient[] ingredientList;
}
