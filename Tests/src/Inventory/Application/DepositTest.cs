using Xunit;
using RestoMatic.Inventory.Application;
using RestoMatic.Inventory.Domain;

namespace Tests.Inventory.Application
{
    public class DepositTest
    {
        [Fact]
        public void ItShouldStoreIngredients()
        {
            // Given
            var deposit = new Deposit("CompanyId");
            var ingredient = new Ingredient("X");
        
            // When
            deposit.Store(ingredient, 1.0, "unit");
        
            // Then
            Assert.True(deposit.HasIngredient(ingredient));
        }
    }
}