if (Test-Path reuters21578) {
  Remove-Item .\reuters21578 -Recurse
}

7z x reuters21578.tar.gz -oreuters21578
7z x reuters21578/reuters21578.tar -oreuters21578

Remove-Item .\reuters21578\reuters21578.tar

Write-Output "Done."