document.addEventListener("DOMContentLoaded", function() {
    const selectHeader = document.getElementById("select-header");
    const selectOptions = document.getElementById("select");
    const sessionCount = document.getElementById("sessionCount");
    const sessionsDiv = document.getElementById("sessions");

    selectHeader.addEventListener("click", function(event) {
        selectOptions.style.display = selectOptions.style.display === "block" ? "none" : "block";
        event.stopPropagation();
    });

    document.addEventListener("click", function(event) {
        selectOptions.style.display = "none";
    });

    selectOptions.addEventListener("click", function(event) {
        event.stopPropagation();
    });

    sessionCount.addEventListener("change", function() {
        const selectedSessionCount = parseInt(sessionCount.value);
        sessionsDiv.innerHTML = ""; // Önceki session alanlarını temizle

        for (let i = 1; i <= selectedSessionCount; i++) {
            const sessionDiv = document.createElement("div");
            sessionDiv.classList.add("session-group");
            sessionDiv.innerHTML = `
          <h3 class="session-header">Session ${i}</h3>
  
          <div class="input-box">
            <input type="text" class="input-field" placeholder="Session Title" required>
          </div>
  
          <div class="input-box">
            <input type="text" class="input-field" placeholder="Session Topic" required>
          </div>
  
          <div class="input-box">
            <label for="presentationType${i}" class="label">Presentation Type</label>
            <select class="input-field" id="presentationType${i}">
              <option value="Oral">Oral</option>
              <option value="Poster">Poster</option>
            </select>
          </div>
  
          <div class="input-box">
            <label for="presenter${i}" class="label">Presenter</label>
            <select class="input-field" id="presenter${i}">
              <!-- Buraya sistemde kayıtlı olan presenterlar dinamik olarak eklenmesi gerekiyor -->
              <!-- Örnek olarak sabit presenter ekliyoruz -->
              <option value="Presenter 1">Presenter 1</option>
              <option value="Presenter 2">Presenter 2</option>
              <option value="Presenter 3">Presenter 3</option>
            </select>
          </div>
  
          <div class="input-box">
            <label for="startTime${i}" class="label">Start Time</label>
            <input type="datetime-local" id="startTime${i}" class="input-field" required>
          </div>
  
          <div class="input-box">
            <label for="endTime${i}" class="label">End Time</label>
            <input type="datetime-local" id="endTime${i}" class="input-field">
          </div>
        `;
            sessionsDiv.appendChild(sessionDiv);
        }
    });

});
  