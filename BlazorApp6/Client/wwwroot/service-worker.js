// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', () => { });
const assets = [
    "/",
    "/index.html",
    "/css/",
    "/webfonts/",
    "/js/",
    "/icon-512.png ",
    "/icon-192.png ",
]
self.addEventListener("install", installEvent => {
    installEvent.waitUntil(
        caches.open(staticDevSite).then(cache => {
            cache.addAll(assets)
        })
    )
});

self.addEventListener("fetch", fetchEvent => {
    fetchEvent.respondWith(
        caches.match(fetchEvent.request).then(res => {
            return res || fetch(fetchEvent.request)
        })
    )
})