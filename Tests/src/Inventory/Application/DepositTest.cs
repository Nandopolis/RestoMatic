using Moq;
using Xunit;
using RestoMatic.Inventory.Application;
using RestoMatic.Inventory.Domain;

namespace Tests.Inventory.Application
{
    public class DepositTest
    {
        string _restaurantId;
        InMemoryInventoryRepository _inventoryRepository;
        Deposit _deposit;

        public DepositTest()
        {
            _restaurantId = "RestaurantId";
            _inventoryRepository = new InMemoryInventoryRepository();
            _deposit = new Deposit(_restaurantId, _inventoryRepository);
        }

        [Fact]
        public void ItShouldStoreIngredients()
        {
            // Given
            var ingredient = new Ingredient("X");

            // When
            _deposit.Store(ingredient, 1.0, "unit");
            var storedIngredient = _deposit.GetIngredientDetails(ingredient);

            // Then
            Assert.IsType<StoredIngredient>(storedIngredient);
            Assert.Equal(1.0, storedIngredient.Quantity, 0.001);
            Assert.Equal("unit", storedIngredient.Unit);
        }

        [Fact]
        public void ItShouldRejectWithdrawingInexistentIngredients()
        {
            // Given
            var ingredient = new Ingredient("X");

            // When
            var exception = Assert.Throws<InsufficientIngredientException>(()
                => _deposit.Withdraw(ingredient, 1.0, "unit"));

            // Then
            Assert.Equal($"Not enough of {ingredient.Name} to withdraw.", exception.Message);
        }

        [Fact]
        public void ItShouldRejectWithdrawingInsufficientIngredients()
        {
            // Given
            var ingredient = new Ingredient("X");
            _inventoryRepository.Update(new StoredIngredient(ingredient, 1.0, "unit"), _restaurantId);

            // When
            var exception = Assert.Throws<InsufficientIngredientException>(()
                => _deposit.Withdraw(ingredient, 2.0, "unit"));

            // Then
            Assert.Equal($"Not enough of {ingredient.Name} to withdraw.", exception.Message);
        }

        [Fact]
        public void ItShouldWithdrawExistingIngredients()
        {
            // Given
            var ingredient = new Ingredient("X");
            _inventoryRepository.Update(new StoredIngredient(ingredient, 2.0, "unit"), _restaurantId);

            // When
            _deposit.Withdraw(ingredient, 1.0, "unit");
            var storedIngredient = _deposit.GetIngredientDetails(ingredient);

            // Then
            Assert.IsType<StoredIngredient>(storedIngredient);
            Assert.Equal(1.0, storedIngredient.Quantity, 0.001);
        }
    }
}