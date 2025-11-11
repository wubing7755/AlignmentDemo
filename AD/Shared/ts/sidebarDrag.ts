/**
 * 侧边栏拖拽功能
 */
function initSidebarDrag(): void {
    const SELECTORS = {
        main: 'main',
        sidebar: '#draggable-sidebar',
        resizeHandle: '.resize-handle',
    } as const; // 使用 as const 使其成为字面量类型

    const DIMENSIONS = {
        minWidth: 210,
        maxWidth: 400,
    };

    const main: HTMLElement | null = document.querySelector(SELECTORS.main);
    const sidebar: HTMLElement | null = document.querySelector(SELECTORS.sidebar);

    // 需要断言 sidebar 不为 null，因为后面要调用 querySelector
    const resizeHandle: HTMLElement | null | undefined = sidebar?.querySelector(SELECTORS.resizeHandle);


    if (!sidebar || !resizeHandle || !main) {
        console.error('Drag elements not found', { main, resizeHandle, sidebar });
        return;
    }

    let isDragging = false;
    let startX = 0;
    let startWidth = 0;
    let translateX = 0;
    let newWidth = 0;

    const handleMouseDown = (e: MouseEvent): void => {
        isDragging = true;
        startX = e.clientX;
        // 使用类型断言，因为我们已经确保 sidebar 存在
        startWidth = parseInt(window.getComputedStyle(sidebar).width, 10);

        document.body.style.cursor = 'col-resize';
        document.body.style.userSelect = 'none';
        e.preventDefault();
    };

    const handleMouseMove = (e: MouseEvent): void => {
        if (!isDragging) return;

        newWidth = Math.max(DIMENSIONS.minWidth, Math.min(DIMENSIONS.maxWidth, startWidth + e.clientX - startX));
        translateX = newWidth - startWidth;

        resizeHandle.style.transform = `translateX(${translateX}px)`;
    };

    const handleMouseUp = (): void => {
        if (!isDragging) return;
        isDragging = false;

        requestAnimationFrame(() => {
            sidebar.style.width = `${newWidth}px`;
            main.style.marginLeft = `${newWidth}px`;
        });

        resizeHandle.style.transform = '';
        document.body.style.cursor = '';
        document.body.style.userSelect = '';
    };

    resizeHandle.addEventListener('mousedown', handleMouseDown);
    document.addEventListener('mousemove', handleMouseMove, { passive: true });
    document.addEventListener('mouseup', handleMouseUp);
}

// 将函数挂载到 window 对象上
window.initSidebarDrag = initSidebarDrag;
