window.sliderScroll = {
    scroll: function (element, direction) {
        if (!element) return;

        const scrollAmount = 220; // Kart genişliği kadar
        element.scrollBy({
            left: direction * scrollAmount,
            behavior: 'smooth'
        });
    }
};
