echo "Starting build for towmsim project"
echo "Dir: $PWD"

DIR=$PWD

xbuild src/replacer.sln /p:Configuration=Release
