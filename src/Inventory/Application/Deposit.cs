using RestoMatic.Inventory.Domain;

namespace RestoMatic.Inventory.Application
{
    public class Deposit
    {
        string restaurantId;

        List<StoredIngredient> inventory = new List<StoredIngredient>();
        
        public Deposit(string restaurantId)
        {
            this.restaurantId = restaurantId;
        }

        public void Store(Ingredient ingredient, double quantity, string unit)
        {
            StoredIngredient? storedIngredient;
            storedIngredient = inventory.Find(item => item.Ingredient.Name == ingredient.Name);
            if (storedIngredient == null)
            {
                storedIngredient = new StoredIngredient(ingredient, 0.0, unit);
                inventory.Add(storedIngredient);
            }
            storedIngredient.Add(quantity, unit);
        }

        public bool HasIngredient(Ingredient ingredient)
        {
            return inventory.Exists(item => item.Ingredient.Name == ingredient.Name);
        }
    }
}