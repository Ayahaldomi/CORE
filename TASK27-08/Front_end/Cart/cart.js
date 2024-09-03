var n = localStorage.getItem("cartID");
var url = `https://localhost:44348/api/Cart/${n}`;
var tbody = document.getElementById("tbody");

var jwtToken = localStorage.getItem("jwtToken"); 
async function allCart() {

    var requist = await fetch(url, {
        headers: {
            'Authorization': `Bearer ${jwtToken}`
        }}
    );
    var response = await requist.json();
    response.forEach(element => {
        var row = document.createElement("tr");
        row.innerHTML = `
        <td><img style="width: 100px;" src="../../Back_end/TASK21-08/Uploads/${element.product.productImage}" alt="${element.product.productImage}"></td>
        <td>${element.product.productName}</td>
        <td>$${element.product.price}</td>
        <td>
        <input type="number" value="${element.quantity}" id="quantity${element.cartItemId}">
        </td>
        <td>
        <button type="button" class="btn btn-primary" onclick="edit(${element.cartItemId})">Edit</button>
        <button type="button" class="btn btn-danger" onclick="deleteItem(${element.cartItemId})">Delete</button>
        </td>
        `;
        tbody.appendChild(row);
    });
};

allCart();


async function edit(id) {
    
    debugger;
    
    event.preventDefault();
    var quantity = document.getElementById(`quantity${id}`);
    var data = {
        quantity: quantity.value
      };
    var url = `https://localhost:44348/api/Cart/${id}`;
    var requist = await fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
    console.log(response);
     location.reload();
}

async function deleteItem(data) {
    var url = `https://localhost:44348/api/Cart/${data}`;
    var requist = await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    location.reload();
    
}
