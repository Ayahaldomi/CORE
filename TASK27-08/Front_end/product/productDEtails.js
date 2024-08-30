
const number = Number(localStorage.getItem('productId'));
const cartId = Number(localStorage.getItem('cartID'));



   var url = `https://localhost:44348/ProductById/${number}`;



async function ProductDetails() {
   

    var requist = await fetch(url);
    let response = await requist.json();

    var container = document.getElementById("contriner");

    console.log(response);
        
    

        response.forEach(element => {
             container.innerHTML += `
        <div class="card" style="width: 18rem;">
        <img src="../../Back_end/TASK21-08/Uploads/${element.productImage}" class="card-img-top" alt="...">
        <div class="card-body">
            <h5 class="card-title">${element.productName}</h5>
                <h5 class="card-title">$${element.price}</h5>

            <p class="card-text">${element.description}</p>
        </div>
        <input type="number" value="1" id="quantity">
        <button type="button" class="btn btn-outline-info" onclick="addToCart()">Add To Cart</button>

        </div>`;
            
        });
        
    
   
    
}

ProductDetails();


async function addToCart() {
    debugger;
    var quantity = document.getElementById('quantity');

    event.preventDefault();
    
    const formData = {
        cartId: cartId,
        productId: number,
        quantity: quantity.value
      }
    var requist = await fetch("https://localhost:44348/api/Cart",{
        method: "POST",
        body:  JSON.stringify(formData),
        headers: {
            'Content-Type': 'application/json'
        }
    });

    var data = requist;
    alert("Product added to cart!");
}

