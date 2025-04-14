export default {
    build: {
        outDir: './wwwroot/css',
        emptyOutDir: true,
        rollupOptions: {
            input: './tailwind-input.css',
            output: {
                assetFileNames: 'site.css'
            }
        }
    }
}