const url = "http://localhost:5081/api/Song";

const render = () => {
    getSongs();
}

///// Get Songs /////
const getSongs = function () {
    fetch(url).then(function (response) {
        return response.json();
    }).then(function(data){
        makeSongTable(data);
    });
};

///// Make Table /////
const makeSongTable = (songs) => {
    let table = document.getElementById("song-table");
    table.innerHTML = "";

    table.appendChild(makeHeader());
    table.appendChild(makeBody(songs));
};

///// MAKE HEADER /////
const makeHeader = () => {
    const header = document.createElement("thead");

    const th1 = document.createElement("th");
    th1.innerHTML = "Title";
    header.appendChild(th1);

    const th2 = document.createElement("th");
    th2.innerHTML = "Artist";
    header.appendChild(th2);

    const th3 = document.createElement("th");
    th3.innerHTML = "Date Added";
    header.appendChild(th3);

    const th4 = document.createElement("th");
    th4.innerHTML = "Favorited";
    header.appendChild(th4);

    const th5 = document.createElement("th");
    th5.innerHTML = "Deleted";
    header.appendChild(th5);

    return header;
}

///// MAKE BODY /////
const makeBody = (songs) => {
    let tbody = document.createElement("tbody");

    songs.forEach((c) => {
        let tr = document.createElement("tr");

        let td1 = document.createElement("td");
        td1.innerHTML = c.title;
        tr.appendChild(td1);

        let td2 = document.createElement("td");
        td2.innerHTML = c.artist;
        tr.appendChild(td2);

        let td3 = document.createElement("td");
        td3.innerHTML = c.dateAdded;
        tr.appendChild(td3);

        let td4 = document.createElement("td");
        td4.innerHTML = c.favorited;
        tr.appendChild(td4);

        let td5 = document.createElement("td");
        td5.innerHTML = c.deleted;
        tr.appendChild(td5);

        let favoriteBtn = document.createElement("button");
        tr.appendChild(favoriteBtn);
        favoriteBtn.innerHTML = "Toggle Favorite";
        favoriteBtn.classList.add("favorite-btn");
        favoriteBtn.addEventListener("click", async () => {
          await toggleFavoriteSong(c.id);
        });

        let editBtn = document.createElement("button");
        tr.appendChild(editBtn);
        editBtn.innerHTML = "Edit";
        editBtn.classList.add("edit-btn");
        editBtn.addEventListener("click", () => {
            let titleInput = document.getElementById("title-input");
            titleInput.value = c.title;

            let artistInput = document.getElementById("artist-input");
            artistInput.value = c.artist;

            let form = document.getElementById("song-form");
            form.onsubmit = editSong;
            form.key = c.id;
        });

        let deleteBtn = document.createElement("button");
        tr.appendChild(deleteBtn);
        deleteBtn.innerHTML = "Delete";
        deleteBtn.classList.add("delete-btn");
        deleteBtn.addEventListener("click", () => {
            deleteSong(c.id);
        });

        tbody.appendChild(tr);
    });
    return tbody;
};


///// CREATE /////
function createNewSong(){
    event.preventDefault();
    // const target = event.target;
    console.log("inside create new song")
    const newSong = {
        title: document.getElementById("title-input").value,
        artist: document.getElementById("title-input").value,
        favorited: false,
        deleted: false
    }
    console.log(newSong)
    fetch(url, {
        method: "POST",
        headers: {
          "Accept": '*/*',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newSong),
    });
    console.log("at the bottom of create new song")
    render();
    // target.title.value = "";
    // target.artist.value = "";
}

const editSong = async (event) => {
    event.preventDefault();
    const target = event.target;
    console.log(target);
    const song = {
        id: target.key,
        title: target.title.value,
        artist: target.artist.value,
        dateAdded: new Date().toISOString(),
    }
    console.log(JSON.stringify(song));
    await fetch(`${url}/${target.key}`, {
        method: 'PUT',
        headers: {
          "Accept": '*/*',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(song),
    });
    render();
    target.title.value = "";
    target.artist.value = "";
};

///// DELETE /////
const deleteSong = async (id) => {
    await fetch(`${url}/${id}`, {
        method: 'DELETE',
        headers: {
          "Accept": '*/*',
          'Content-Type': 'application/json',
        },
    });
    render();
}

const toggleFavoriteSong = async (id) => {
    console.log("Toggling favorite for song ID:", id);
  
    // Fetch the current song data
    const response = await fetch(`${url}/${id}`);
    if (response.ok) {
      const song = await response.json();
      console.log("Fetched song:", song);
  
      // Update the favorited value
      song.favorited = !song.favorited;
  
      // Send the updated song data to the server
      await fetch(`${url}/${id}`, {
        method: "PUT",
        headers: {
          "Accept": "*/*",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(song),
      });
      console.log("past fetch");
      render();
    } else {
      //console.error("Error fetching song:", response.statusText);
      console.log("Its an error....")
    }
};
  

render();