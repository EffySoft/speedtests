namespace SpeedTest {
    public static class SizeConverter {
        public static double ConvertBytesToMegabytes (long bytes) {
            return (bytes / 1024f) / 1024f;
        }

        public static double ConvertKilobytesToMegabytes (long kilobytes) {
            return kilobytes / 1024f;
        }
    }
}