%% GENERATE INPUT FILES FOR BONSAI SPHERE STIMULATION
%  created @yiranhe
%%
root = 'E:\code\vis-stim\Yiran\vis-stim-depth\Spheres\';
write2folder = 'Files_inputs\';
root = [root,write2folder];

%% Which one to run?
generate_translationXYZ = 1;
generate_ambient_values = 1;

%% GENERATE A SERIES OF LOCATIONS (translation XYZ) FOR MULTIPLE SPHERES TO DISPLAY
if generate_translationXYZ
    
    % Set values 
    tsl.x.n = 5; %how many x positions
    tsl.y.n = 5; %how many y positions
    tsl.z.n = 3; %how many z positions

    tsl.x.MAX = 5;
    tsl.x.MIN = -5;
    tsl.y.MAX = 5;
    tsl.y.MIN = -5;
    tsl.z.MAX = 1;
    tsl.z.MIN = 5;

    tsl.x.filename = 'TranslationX_multi';
    tsl.y.filename = 'TranslationY_multi';
    tsl.z.filename = 'TranslationZ_multi';
    
    % All xyz values
    tsl.x.values = linspace(tsl.x.MIN,tsl.x.MAX,tsl.x.n);
    tsl.y.values = linspace(tsl.y.MIN,tsl.y.MAX,tsl.y.n);
    tsl.z.values = linspace(tsl.z.MIN,tsl.z.MAX,tsl.z.n);
    
    
    % Create all combos of xyz
    tsl.xyz.values = [];
    for this_x = tsl.x.values
        for this_y = tsl.y.values
            for this_z = tsl.z.values
                tsl.xyz.values = [tsl.xyz.values;this_x,this_y,this_z];
            end
        end
    end
    
    
    % Save Translation XYZ to separate csv files 
    tsl.xyz.T = array2table(tsl.xyz.values);
    writetable(tsl.xyz.T(:,1),[root,tsl.x.filename,'.csv'],'WriteVariableNames',0);
    writetable(tsl.xyz.T(:,2),[root,tsl.y.filename,'.csv'],'WriteVariableNames',0);
    writetable(tsl.xyz.T(:,3),[root,tsl.z.filename,'.csv'],'WriteVariableNames',0);
    
    
end


%% FOR VISUAL STIMULI TO FADE IN
% Generate a series of ambient values
if generate_ambient_values
    
    % Set values 
    FRAMERATE = 100; %Hz
    N_objects = tsl.x.n * tsl.y.n * tsl.z.n ; %num of objects displayed
    INITIAL_DELAY = randi([1 5],1,N_objects); %s
    XLvalue = 1000; %a very large value for total matrix storage 

    ambient.START = 0.664; %for background in DARKGREY
    ambient.END = 1;
    ambient.SUSTAIN = 1;

    ambient.fadein_time = 0.5; %s
    ambient.fadeout_time = 0.5; %s
    ambient.total_time = 4; %S
    ambient.sustain_time = ambient.total_time - ambient.fadein_time - ambient.fadeout_time;
    ambient.filename = 'Ambient_values_multi';


    % Create an "empty" matrix to store ambience values of all objects
    ambient.M = ambient.START * ones(N_objects,XLvalue);

    %Loop through all objects
    for iobj = 1:N_objects

        ambient.delay_time = INITIAL_DELAY(iobj);

        % Generate a series of ambient values
        ambient.initialarr = [ones(1,ambient.delay_time * FRAMERATE) * ambient.START];
        ambient.arr1 = [linspace(ambient.START,ambient.END,(FRAMERATE * ambient.fadein_time))];
        ambient.arr2 = [ones(1,ambient.sustain_time * FRAMERATE+1) * ambient.SUSTAIN];
        ambient.arr3 = [linspace(ambient.END,ambient.START,(FRAMERATE * ambient.fadeout_time))];
        ambient.arr = [ambient.initialarr,ambient.arr1,ambient.arr2,ambient.arr3];

        % Write to total matrix
        ambient.M(iobj,1:size(ambient.arr,2)) = ambient.arr;
        clear ambient.arr;
    end

    % Save to csv
    %ambient.M = ambient.M';  % each trial (displaying all spheres) is a row, each column is a time series of changing ambience of one sphere
    ambient.T = array2table(ambient.M);
    writetable(ambient.T,[root,ambient.filename,'.csv'],'WriteVariableNames',0);

end
