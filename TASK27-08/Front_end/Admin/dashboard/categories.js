const url = "https://localhost:44348/api/Category";

async function allCategory() {

    var requist = await fetch(url);
    var response = await requist.json();

    var tBody = document.getElementById("tBody");
    var counter = 1;
    response.forEach(element => {
        


        tBody.innerHTML += `
 

<tr>
                <th scope="row">${counter++}</th>
                <td>${element.categoryName}</td>
                <td><img style="width: 100px;" src="../../../Back_end/TASK21-08/Uploads/${element.categoryImage}" alt="Card image cap"></td>
                <td><a href="updateCAtegory.html" class="btn btn-primary" onclick="storeData(${element.categoryId})">Edit</a></td>
              </tr>

    `
    });
    
}

function storeData(data){
  
    localStorage.setItem("categoryID", data);
    console.log("hi")
}

allCategory();