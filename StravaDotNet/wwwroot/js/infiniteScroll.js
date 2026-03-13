window.setupInfiniteScroll = function (element, dotNetHelper) {
    if (!element) {
        console.warn("setupInfiniteScroll: element is null");
        return;
    }

    const options = {
        root: null,
        rootMargin: '200px',
        threshold: 0.1
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach((entry) => {
            if (entry.isIntersecting) {
                // Call the instance method on the DotNetObjectReference
                dotNetHelper.invokeMethodAsync('LoadMoreWeeks').catch(err => console.error('invokeMethodAsync error', err));
            }
        });
    }, options);

    observer.observe(element);

    // store so we can disconnect later
    window._infiniteScrollObserver = observer;
    window._infiniteScrollDotNet = dotNetHelper;
};

window.disposeInfiniteScroll = function () {
    if (window._infiniteScrollObserver) {
        window._infiniteScrollObserver.disconnect();
        window._infiniteScrollObserver = null;
    }
    if (window._infiniteScrollDotNet) {
        // The DotNetObjectReference should be disposed on the .NET side; remove JS reference
        window._infiniteScrollDotNet = null;
    }
};