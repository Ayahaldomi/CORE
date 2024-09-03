
const number = Number(localStorage.getItem('categoryID'));

if (number == 0) {
    var url = "https://localhost:44348/api/Products";
}
else{
   var url = `https://localhost:44348/ProductByCategoryId/${number}`;
}
var jwtToken = localStorage.getItem("jwtToken"); 


async function allProducts() {
    if(jwtToken == null) {
        alert("Please Login First");
    }
    debugger

    var requist = await fetch(url, {
        headers: {
            'Authorization': `Bearer ${jwtToken}`
        }});
    var response = await requist.json();

    var container = document.getElementById("contriner");

    response.forEach(element => {

        container.innerHTML += `


<div class="col">
                <div class="card h-100" style="width: 18rem;">
                    <img src="../../Back_end/TASK21-08/Uploads/${element.productImage}" class="card-img-top" alt="...">
                    <div class="card-body">
                        <h5 class="card-title">${element.productName}</h5>
                        <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                        <a href="productDEtails.html" class="btn btn-primary" onclick="productID(${element.productId})">Go to Product</a>
                    </div>
                </div>
            </div>


`
        
    
    });
    
}

allProducts();

function productID(data){

    localStorage.setItem("productId", data);
}