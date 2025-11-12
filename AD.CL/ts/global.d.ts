declare global {
    interface Window {
        initSidebarDrag?: () => void;
        initThemeToggle?: () => void;

        ScriptsInOne: {
            initialize(): void;
            cleanup(fullCleanup?: boolean): void;
            forcecleanup(): void;
        };
    }
}

export { };
