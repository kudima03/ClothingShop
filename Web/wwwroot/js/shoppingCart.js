const getUri = "/shoppingCarts/GetUserShoppingCartData";
const putUri = "/shoppingCarts";
const ordersUri = "/orders";
function changeProductOptionQuantity(productOptionId, increment) {

    var totalSum = 0;
    fetch(getUri)
        .then((response) => response.json())
        .then((json) => {

            var itemToEdit = json.items.find(x => x.productOptionId === productOptionId);

            var quantity = parseInt(document.getElementById("quantityOf " + productOptionId).value, 0);

            itemToEdit.amount = quantity;

            var requestItems = [];

            json.items.forEach(x => {
                requestItems.push({
                    "ProductOptionId": x.productOptionId,
                    "Quantity": x.amount
                });
                totalSum += x.amount * x.productOption.price;
            });

            var requestBody = {
                "UserId": 0,
                "ItemsDtos": requestItems
            }

            return requestBody;
        })
        .then((requestBody) => {
            fetch(putUri,
                {
                    method: "PUT",
                    headers: {
                        'Content-Type': "application/json"
                    },
                    body: JSON.stringify(requestBody)
                }).then((response) => {
                    if (!response.ok) {
                        if (increment) {
                            document.getElementById("quantityOf " + productOptionId).value--;
                        } else {
                            document.getElementById("quantityOf " + productOptionId).value++;
                        }
                    } else {
                        document.getElementById("totalPrice").innerHTML = "$" + totalSum;
                    }
                });
        });
}

function addNewProductOptionToCart(productOptionId) {
    fetch(getUri)
        .then((response) => response.json())
        .then((json) => {

            var requestItems = [];

            json.items.forEach(x => {
                requestItems.push({
                    "ProductOptionId": x.productOptionId,
                    "Quantity": x.amount
                });
            });

            if (requestItems.find(x => x.ProductOptionId === productOptionId) == null) {
                requestItems.push({
                    "ProductOptionId": productOptionId,
                    "Quantity": 1
                });
            }

            var requestBody = {
                "UserId": 0,
                "ItemsDtos": requestItems
            }

            return requestBody;
        })
        .then((requestBody) => {
            fetch(putUri,
                {
                    method: "PUT",
                    headers: {
                        'Content-Type': "application/json"
                    },
                    body: JSON.stringify(requestBody)
                });
        });
}

function removeProductOptionFromCart(productOptionId) {
    fetch(getUri)
        .then((response) => response.json())
        .then((json) => {

            var requestItems = [];

            json.items.forEach(x => {

                if (x.productOptionId !== productOptionId) {
                    requestItems.push({
                        "ProductOptionId": x.productOptionId,
                        "Quantity": x.amount
                    });
                }
            });

            var requestBody = {
                "UserId": 0,
                "ItemsDtos": requestItems
            }

            console.log(requestBody);

            return requestBody;
        })
        .then((requestBody) => {
            fetch(putUri,
                {
                    method: "PUT",
                    headers: {
                        'Content-Type': "application/json"
                    },
                    body: JSON.stringify(requestBody)
                })
                .then(() => {
                    location.reload();
                });
        });
}

function createOrderFromCartItems() {

    fetch(getUri)
        .then((response) => response.json())
        .then((json) => {

            var requestItems = [];

            json.items.forEach(x => {
                requestItems.push(x.id);
            });

            var requestBody = {
                "UserId": 0,
                "ShoppingCartItemsIds": requestItems
            }

            return requestBody;

        }).then((requestBody) => {
            fetch(ordersUri,
                {
                    method: "POST",
                    headers: {
                        'Content-Type': "application/json"
                    },
                    body: JSON.stringify(requestBody)
                }).then(() => {
                    location.reload();
                });
        });
}