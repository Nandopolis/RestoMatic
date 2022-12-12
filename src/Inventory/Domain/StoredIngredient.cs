namespace RestoMatic.Inventory.Domain
{
    public class StoredIngredient
    {
        public Ingredient Ingredient { get; set; }

        public double Quantity { get; set; }
        public string Unit { get; set; }

        public StoredIngredient(Ingredient ingredient, double quantity, string unit)
        {
            Ingredient = ingredient;
            Quantity = quantity;
            Unit = unit;
        }

        public void Add(double quantity, string unit)
        {
            if (unit != Unit)
            {
                throw new ArgumentException();
            }
            Quantity += quantity;
        }

        public void Substract(double quantity, string unit)
        {
            if (unit != Unit)
            {
                throw new ArgumentException();
            }
            if (Quantity < quantity)
            {
                throw new InsufficientIngredientException(Ingredient);
            }
            Quantity -= quantity;
        }
    }
}