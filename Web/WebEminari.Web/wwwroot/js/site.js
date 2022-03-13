let DateTime = luxon.DateTime;
!function () { const e = document, t = e.documentElement, n = e.body, i = e.getElementById("lights-toggle"), s = window.sr = ScrollReveal(); function a() { let e = i.parentNode.querySelector(".label-text"); i.checked ? (n.classList.remove("lights-off"), e && (e.innerHTML = "dark")) : (n.classList.add("lights-off"), e && (e.innerHTML = "light")) } t.classList.remove("no-js"), t.classList.add("js"), window.addEventListener("load", function () { n.classList.add("is-loaded") }), n.classList.contains("has-animations") && window.addEventListener("load", function () { s.reveal(".feature", { duration: 600, distance: "20px", easing: "cubic-bezier(0.215, 0.61, 0.355, 1)", origin: "right", viewFactor: .2 }) }), i && (window.addEventListener("load", a), i.addEventListener("change", a)) }();
(function () {
    let timeTags = [...document.getElementsByTagName("time")];

    timeTags.forEach((e) => {
        let dateTime = e.getAttribute("datetime");
        if (!dateTime) {
            return;
        }

        let time = DateTime.fromISO(dateTime, { zone: "utc", locale: "bg" });
        let localized = time.toLocal().toLocaleString(DateTime.DATETIME_MED);

        e.innerHTML = localized;
        e.setAttribute("localtime", localized);
	})
}());

function escapeHtml(unsafe)
{
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}
function toggleLike(webEminarId) {
    let token = document.querySelector("#likesForm input[name=__RequestVerificationToken]").value;
    let target = document.getElementById("likesCount");
    let body = { WebEminarID : webEminarId };

    fetch('/api/Likes', {
        method: 'POST',
        headers: {
            'X-CSRF-TOKEN': token,
            'Content-Type': 'application/json; charset=utf-8',
        },
        body: JSON.stringify(body),
    })
        .then(response => response.json())
        .then(data => {
            target.innerHTML = data['likesCount'];
            return data;
        })
        .catch(err => console.error(err));
}
		