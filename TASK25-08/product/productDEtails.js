
const number = Number(localStorage.getItem('productId'));


   var url = `https://localhost:44348/ProductById/${number}`;



async function ProductDetails() {
   

    var requist = await fetch(url);
    let response = await requist.json();

    var container = document.getElementById("contriner");

    console.log(response);
        
        // container.innerHTML = `
        // <div class="card" style="width: 18rem;">
        // <img src="${response[0].productImage}" class="card-img-top" alt="...">
        // <div class="card-body">
        //     <h5 class="card-title">${response[0].productName}</h5>
        //         <h5 class="card-title">${response[0].price}</h5>

        //     <p class="card-text">${response[0].description}</p>
        // </div>
        // </div>`;

        response.forEach(element => {
             container.innerHTML += `
        <div class="card" style="width: 18rem;">
        <img src="${element.productImage}" class="card-img-top" alt="...">
        <div class="card-body">
            <h5 class="card-title">${element.productName}</h5>
                <h5 class="card-title">$${element.price}</h5>

            <p class="card-text">${element.description}</p>
        </div>
        </div>`;
            
        });
        
    
   
    
}

ProductDetails();

