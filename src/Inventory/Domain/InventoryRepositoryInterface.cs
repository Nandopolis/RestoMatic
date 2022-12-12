namespace RestoMatic.Inventory.Domain
{
    public interface InventoryRepositoryInterface
    {
        public StoredIngredient? FindStoredIngredient(Ingredient ingredient, string restaurantId);
        public void Update(StoredIngredient storedIngredient, string restaurantId);
    }
}
