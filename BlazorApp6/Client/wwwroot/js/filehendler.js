let fileHandle;

window.OpenFile = async () => {
    fileHandle = await window.showOpenFilePicker();

    const file = await fileHandle[0].getFile();
    const contents = await file.text();
    return contents;
}
window.SaveFile = async (contents) => {
    fileHandle = await window.showSaveFilePicker();

    // Create a FileSystemWritableFileStream to write to.
    const writable = await fileHandle[0].createWritable();
    // Write the contents of the file to the stream.
    await writable.write(contents);
    // Close the file and write the contents to disk.
    await writable.close();
}