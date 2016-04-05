use File::stat;

my $md5='E:\EasyJoy_P0102_D140522\trunk\CodeProject\client\Tools\md5\Release\md5.exe';

sub LogToFile
{
	my $text1=shift;
	open TXT,">>a.txt"; 
	print TXT "$text1";     #  如果文件a.txt已存在，该行内容会被附加到已存在文件的后面
	print TXT "\n";
	close TXT;
}

sub generateServerMd5Lua 
{
	my $imagesDir=shift;
	my $scriptDir=shift;
	my $fontsDir=shift;
	my $soundDir=shift;
	my $serverversion=shift;
	my $majorVersion = shift;
	my $minorVersion = shift;
	my $resourceName=shift;
	my $serverpath=shift;

	# print($imagesDir,scriptDir)
	print "generateServerMd5Lua\n";
	
	unlink "a.txt";
	unlink "serverversion.lua";

	# LogToFile(imagesDir);



	
	if (!$serverpath) 
	{
		open FH,">$serverversion" or die "open $serverversion failed\n";
		print FH "ServerVersion = {majorVersion=\"$majorVersion\", minorVersion=\"$minorVersion\", resourceList={\n";
		close FH;
		
		# md5AllFiles($fontsDir, $serverversion, 1);		
		md5AllFiles($imagesDir, $serverversion, 1);
		# md5AllFiles($scriptDir, $serverversion, 1);
		# md5AllFiles($soundDir, $serverversion, 1);
	
		open FH,">>$serverversion" or die "open $serverversion failed\n";
		print FH "},\n";
		print FH "}\n";
		close FH;
	}
	# else 
	# {
	# 	copyFile($serverpath, $serverversion);
		
	# 	mad5NewFiles($fontsDir, $serverversion, 1, $resourceName);		
	# 	mad5NewFiles($imagesDir, $serverversion, 1, $resourceName);
	# 	mad5NewFiles($scriptDir, $serverversion, 1, $resourceName);
	# 	mad5NewFiles($soundDir, $serverversion, 1, $resourceName);
	# }

	for(my $i=0;$i<10000000000000;$i++)
	{
    	print $i;
    	print "\n";
	}

}

sub md5AllFiles 
{
	my $dir = shift;
	my $luaTmpFile = shift;
	my $serverVersion = shift;
	
	if(!(-e $dir)) 
	{
		return;
	}
	local *DH;
    if (opendir(DH, $dir)) 
    {
     	foreach (readdir(DH)) 
     	{
			if ($_ eq "." || $_ eq "..") 
			{
				next;
			}
			md5AllFiles($dir."\\".$_, $luaTmpFile, $serverVersion);
        }
        closedir(DH);
     }
     else 
     {
     	my $md5Value = getMd5Value($dir);
     	my $key=trimHead($dir, "$resourceDir\\");
		LogToFile("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! $dir $md5Value $key");

		 open FFH,">>$luaTmpFile";
		 print FFH "[\"$key\"]={[\"md5\"]=\"$md5Value\",";
		 
		 if ($serverVersion == 1) 
		 {
		 	my @array = stat($dir);
		 	my $size=stat($dir)->size;
		 	print FFH "[\"size\"]=$size,";
		 }
		 
		 print FFH "},\n";
		 close FFH;
     }
}


sub mad5NewFiles 
{
	my $dir = shift;
	my $luaTmpFile = shift;
	my $serverVersion = shift;
	my $resourceName = shift;
	
	 local *DH;
     if (opendir(DH, $dir)) 
     {
     	foreach (readdir(DH)) 
     	{
			if ($_ eq "." || $_ eq "..") 
			{
				next;
			}
			mad5NewFiles($dir."\\".$_, $luaTmpFile, $serverVersion, $resourceName);
        }
        closedir(DH);
     }
     else 
     {
     	my $md5Value = getMd5Value($dir);
     	$_=$dir;
     	/(.*)(-)(.*)/;
     	$_ = $3;
		s/\\/\//g;
		my $key = $_;
		 
		my $tmpfile = $luaTmpFile.".tmp";
     	copyFile($luaTmpFile, $tmpfile);
	     if (findStr($luaTmpFile, $key) == 1) 
	     {
     		open FH,"<$tmpfile";
     		open FFH,">$luaTmpFile";
     		while(<FH>) 
     		{
     			if ($serverVersion == 1) 
     			{
     				if (/(.*$key.*)(,}.*)/) 
     				{
     					my $newline = $1.",[\"".$resourceName."\"]=\"$md5Value\"".$2."\n";
     					print FFH $newline;
     				}
     				else 
     				{
     					print FFH $_;
     				}
     			}
     			else
     			{
     				if (/(.*$key.*md5\"\]=\")(.*)(\",.*)/) 
     				{
     					my $newline = $1.$md5Value.$3."\n";
     					print FFH $newline;
     				}
     				elsif (/(.*resourceName=\")(.*)(\",.*)/) 
     				{
     					print FFH $1.$resourceName.$3."\n";
     				}
     				else
     				{
     					print FFH $_;
	     			}
     			}
     		}
     		close FH;
     		close FFH;
     	}
     	else 
     	{
     		open FH,"<$tmpfile";
     		open FFH,">$luaTmpFile";
     		my $count = 0;
     		while(<FH>) 
     		{
     			if ($serverVersion == 1) 
     			{
     				print FFH $_;
     			
     				if ($count == 0) 
     				{
     					print FFH "[\"$key\"]={[\"$resourceName\"]=\"$md5Value\",";
     				 
		 				my @array = stat($dir);
		 				my $size=stat($dir)->size;
		 				print FFH "[\"size\"]=$size,},\n";
     				}
     			}
     			else 
     			{
     				if (/(.*resourceName=\")(.*)(\",.*)/) 
     				{
     					print FFH $1.$resourceName.$3."\n";
     				}
     				else 
     				{
	   					print FFH $_;
     				}
     			
     				if ($count == 0) 
     				{     				
     					print FFH "[\"$key\"]={[\"md5\"]=\"$md5Value\",";
		 				print FFH "},\n";
     				}
     			}

     			$count = $count + 1;
     		}
     		close FH;
     		close FFH;
     	}
     	unlink $tmpfile;
     }
} 

sub getMd5Value 
{
	my $filepath = shift;
	my $md5Value;

	my $tmpfile="tmpfile.txt";
	$_ = $filepath;
	if (/(\.pvr\.ccz)/i) 
	{
   		system "$md5 $filepath 5 >> $tmpfile";
   	}
   	else 
   	{
   		system "$md5 $filepath >> $tmpfile";
   	}
	open FH,"<$tmpfile";
	while(<FH>) 
	{
		chomp;
		$md5Value = $_;
		last;
	}
	close FH;
	unlink "$tmpfile";
	
	print "md5 $filepath\n";
	return $md5Value;
}

sub trimHead {
	my $fullPath = shift;
	my $head = shift;

#	my $_ = substr($fullPath, length($head), length($fullPath) - length($head));
#	s/\\/\//g;
	$_=$fullPath;
	if (/(.*\\)(images\\.*)/) {
		$_ = $2;
	}
	elsif (/(.*\\)(script\\.*)/) {
		$_ = $2;
	}
	elsif (/(.*\\)(fonts\\.*)/) {
		$_ = $2;
	}
	elsif (/(.*\\)(sound\\.*)/) {
		$_ = $2;
	}
	elsif (/(.*\\)(md5test\\.*)/) {
		$_ = $2;
	}
	
	s/\\/\//g;
	
	return $_;
}


my $imagesDir = 'E:\\WorkSpace_Unity\\HelloWorld_Perl\\md5test';#$gameAssets."\\images";
my $fontsDir = $gameAssets."\\fonts";
my $soundDir = $gameAssets."\\sound";
my $scriptDir = $gameAssets."\\script";
my $serverversion="serverversion.lua";
my $majorVersion = 1.2;
my $minorVersion = 16400;
my $resourceName = "general";

generateServerMd5Lua($imagesDir, $scriptDir, $fontsDir, $soundDir, $serverversion, $majorVersion, $minorVersion, $resourceName);