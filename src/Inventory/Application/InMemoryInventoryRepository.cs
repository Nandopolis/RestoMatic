namespace RestoMatic.Inventory.Domain
{
    public class InMemoryInventoryRepository : InventoryRepositoryInterface
    {
        Dictionary<string, List<StoredIngredient>> _inventory = new();

        public StoredIngredient? FindStoredIngredient(Ingredient ingredient, string restaurantId)
        {
            validateRestaurantId(restaurantId);
            return _inventory[restaurantId].Find(item => item.Ingredient.Name == ingredient.Name);
        }

        public void Update(StoredIngredient storedIngredient, string restaurantId)
        {
            validateRestaurantId(restaurantId);
            var _storedIngredient = FindStoredIngredient(storedIngredient.Ingredient, restaurantId);
            if (_storedIngredient == null)
            {
                _inventory[restaurantId].Add(storedIngredient);
            }
            else
            {
                _storedIngredient.Quantity = storedIngredient.Quantity;
                _storedIngredient.Unit = storedIngredient.Unit;
            }
        }

        private void validateRestaurantId(string restaurantId)
        {
            if (!_inventory.ContainsKey(restaurantId))
            {
                _inventory.Add(restaurantId, new List<StoredIngredient>());
            }
        }
    }
}