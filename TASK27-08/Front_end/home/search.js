var search = document.getElementById('search');

async function search() {
    debugger;
    var url = `https://localhost:44348/api/Products?search=${search.value}`;
    var requist = await fetch(url);
    var response = await requist.json();
    

    var getsearch = document.getElementById('getsearch');
    getsearch.innerHTML = `
    <div class="card" style="width: 18rem;">
  <img src="..." class="card-img-top" alt="...">
  <div class="card-body">
    <h5 class="card-title">User Name ${response.username}</h5>
        <h5 class="card-title">User Password ${response.password}</h5>
    <h5 class="card-title">User ID ${response.userId}</h5>
  </div>
</div>
    `
}