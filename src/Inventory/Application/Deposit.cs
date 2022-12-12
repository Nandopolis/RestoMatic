using RestoMatic.Inventory.Domain;

namespace RestoMatic.Inventory.Application
{
    public class Deposit
    {
        string _restaurantId;

        InventoryRepositoryInterface _inventoryRepository;

        public Deposit(string restaurantId, InventoryRepositoryInterface inventoryRepository)
        {
            _restaurantId = restaurantId;
            _inventoryRepository = inventoryRepository;
        }

        public void Store(Ingredient ingredient, double quantity, string unit)
        {
            StoredIngredient? storedIngredient =
                _inventoryRepository.FindStoredIngredient(ingredient, _restaurantId);
            if (storedIngredient == null)
            {
                storedIngredient = new StoredIngredient(ingredient, 0.0, unit);
            }
            storedIngredient.Add(quantity, unit);
            _inventoryRepository.Update(storedIngredient, _restaurantId);
        }

        public void Withdraw(Ingredient ingredient, double quantity, string unit)
        {
            var storedIngredient = _inventoryRepository.FindStoredIngredient(ingredient, _restaurantId);
            if (storedIngredient == null)
            {
                throw new InsufficientIngredientException(ingredient);
            }
            storedIngredient.Substract(quantity, unit);
            _inventoryRepository.Update(storedIngredient, _restaurantId);
        }

        public StoredIngredient? GetIngredientDetails(Ingredient ingredient)
        {
            return _inventoryRepository.FindStoredIngredient(ingredient, _restaurantId);
        }
    }
}