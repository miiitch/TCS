"use strict";
// ReSharper disable InconsistentNaming
interface IPrice {    
    Delivery: number;
    Products: number;   
}

interface ICart {
    
    Products: ICartProduct [];

}


interface ICartProduct {
    Id: string;
    Count: number;

}
// ReSharper restore InconsistentNaming

declare var cartApiUrl: string;


function updatePrice(price: IPrice) {
    $("#delivery").text(`${price.Delivery} €`);
    $("#products").text(`${price.Products} €`);
    var total = price.Products + price.Delivery;
    $("#total").text(`${total} €`);
}

function updateItem(cartLine: JQuery, productId: string, cart: ICart) {
    var productData = cart.Products.filter(p => p.Id === productId);

    if (productData.length === 0) {
        $(cartLine).remove();
    } else {
        $(cartLine).children(".count").text(productData[0].Count);
    }
}

function changeCart(cartLine: JQuery, command: string): void {
    var productId = $(cartLine).data("productid");

    $.ajax
        ({
            type: "POST",
            url: cartApiUrl,
            dataType: "json",
            contentType: "application/json",
            async: false,
            //json object to sent to the authentication url
            data: `{"command": "${command}", "productId" : "${productId}"}`,
            success(cart: ICart) {             
                updateItem(cartLine, productId, cart);
                $.ajax
                    ({
                        type: "Get",
                        url: cartApiUrl + "/price",
                        dataType: "json",
                        async: false,
                        success(priceData: IPrice) {
                            updatePrice(priceData);

                        }
                    });

            }
        });
}

$(() => {
    $("a.addbtn").click(evt => {
        var cartLine = $(evt.currentTarget).parent().parent();
        changeCart(cartLine, "add");


    });
    $("a.removebtn").click(evt => {
        var cartLine = $(evt.currentTarget).parent().parent();
        changeCart(cartLine, "remove");
    });
});