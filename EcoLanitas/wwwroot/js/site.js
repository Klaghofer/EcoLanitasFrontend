// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    // Navegación entre secciones
    const navButtons = document.querySelectorAll('.nav-btn');
    const sections = document.querySelectorAll('.section');
    const minPriceInput = document.getElementById('minPrice');
    const maxPriceInput = document.getElementById('maxPrice');
    const filterMessage = document.getElementById('filterMessage');
    const galleryItems = document.querySelectorAll('.gallery-item');

    navButtons.forEach(btn => {
    btn.addEventListener('click', () => {
        const sectionId = btn.dataset.section;

        navButtons.forEach(b => b.classList.remove('active'));
        btn.classList.add('active');

        sections.forEach(s => s.classList.remove('active'));
        document.getElementById(sectionId).classList.add('active');

        window.scrollTo({top: 0, behavior: 'smooth'});
    });
});

    // Filtro de catálogo por precio. Puede ampliarse con atributos data-
    // adicionales, por ejemplo: data-size, data-color o data-category.
    function filterProductsByPrice() {
    const minPrice = minPriceInput.value === '' ? 0 : Number(minPriceInput.value);
    const maxPrice = maxPriceInput.value === '' ? Infinity : Number(maxPriceInput.value);

    if (minPrice > maxPrice) {
    filterMessage.textContent = 'El precio mínimo no puede ser mayor que el máximo.';
    return;
}

    let visibleProducts = 0;
    galleryItems.forEach(item => {
    const price = Number(item.dataset.price);
    const isVisible = price >= minPrice && price <= maxPrice;
    item.hidden = !isVisible;
    if (isVisible) visibleProducts++;
});

    filterMessage.textContent = visibleProducts
    ? `${visibleProducts} prenda${visibleProducts === 1 ? '' : 's'} encontrada${visibleProducts === 1 ? '' : 's'}.`
    : 'No hay prendas en ese rango de precio.';
}

    document.getElementById('filterByPrice').addEventListener('click', filterProductsByPrice);
    document.getElementById('clearPriceFilter').addEventListener('click', () => {
    minPriceInput.value = '';
    maxPriceInput.value = '';
    galleryItems.forEach(item => item.hidden = false);
    filterMessage.textContent = '';
});

    [minPriceInput, maxPriceInput].forEach(input => {
    input.addEventListener('keydown', event => {
        if (event.key === 'Enter') filterProductsByPrice();
    });
});

    // Formateo automático de número de tarjeta
    document.getElementById('cardNumber').addEventListener('input', function(e) {
    let value = e.target.value.replace(/\s/g, '');
    let formattedValue = value.match(/.{1,4}/g)?.join(' ') || value;
    e.target.value = formattedValue;
});

    // Formateo de fecha de expiración
    document.getElementById('expiry').addEventListener('input', function(e) {
    let value = e.target.value.replace(/\D/g, '');
    if (value.length >= 2) {
    value = value.slice(0, 2) + '/' + value.slice(2, 4);
}
    e.target.value = value;
});

    // Solo números en CVV
    document.getElementById('cvv').addEventListener('input', function(e) {
    e.target.value = e.target.value.replace(/\D/g, '');
});

    // Manejo del formulario
    document.getElementById('orderForm').addEventListener('submit', function(e) {
    e.preventDefault();

    // Aquí se procesaría el pedido
    alert('¡Pedido recibido! Nos pondremos en contacto contigo pronto para confirmar los detalles.');

    // Reiniciar formulario
    this.reset();
});
