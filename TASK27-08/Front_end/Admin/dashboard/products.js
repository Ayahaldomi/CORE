

    var url = "https://localhost:44348/api/Products";




async function allProducts() {
    debugger

    var requist = await fetch(url);
    var response = await requist.json();

    var tBody = document.getElementById("tBody");
    var counter = 1;
    response.forEach(element => {

        tBody.innerHTML += `

<tr>
                <th scope="row">${counter++}</th>
                <td>${element.productName}</td>
                <td><img style="width: 100px;" src="../../../Back_end/TASK21-08/Uploads/${element.productImage}" alt="${element.productImage}"></td>
                <td>${element.price}</td>
                <td>${element.description}</td>

                <td><a href="updateProduct.html" class="btn btn-primary" onclick="productID(${element.productId})">Edit</a></td>
              </tr>


`
        
    
    });
    
}

allProducts();

function productID(data){
debugger;
    localStorage.setItem("productId", data);
}