namespace RestoMatic.Inventory.Domain
{
    public class InsufficientIngredientException : Exception
    {
        public InsufficientIngredientException(Ingredient ingredient)
            : base($"Not enough of {ingredient.Name} to withdraw.")
        {
        }
    }
}