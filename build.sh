echo "Starting build for towmsim project"
echo "Dir: $PWD"

DIR=$PWD

xbuild src/townsim.sln /p:Configuration=Release
