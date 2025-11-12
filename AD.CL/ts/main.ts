interface IScriptsInOne {
    initialize(): void;
    cleanup(fullCleanup?: boolean): void;
    forcecleanup(): void;
}

export const ScriptsInOne: IScriptsInOne = (() => {
    return {
        /**
         * 初始化所有脚本功能
         */
        initialize(): void {
            console.log('Initializing scripts...');

            if (typeof window.initSidebarDrag === 'function') {
                window.initSidebarDrag();
            }

            if (typeof window.initThemeToggle === 'function') {
                window.initThemeToggle();
            }

            window.addEventListener('beforeunload', () => ScriptsInOne.cleanup());
        },

        /**
         * 清理当前主题设置
         * @param fullCleanup 是否执行完整清理
         */
        cleanup(_fullCleanup?: boolean): void {
            document.documentElement.removeAttribute('data-theme');
        },

        /**
         * 强制清理：清除 localStorage 中的主题设置
         */
        forcecleanup(): void {
            this.cleanup();
            localStorage.removeItem('theme');
        },
    };
})();

// 挂载到 window
window.ScriptsInOne = ScriptsInOne;
